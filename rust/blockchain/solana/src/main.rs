// what it does
// A.
// 1. uses exiting account with native tokens (created via cli)
// 2. creates new account using existing token program for native mint
// B.
// 1. creates new token
// 2. creates account for that token
// 3. mints token
//
// notes: not is just plain `just to the thing`, not good desing or code org, just blockchain and raw rust (so you see ugly code)
//
// SAMPLE OUTPUT:
// Running `target\debug\solana-test.exe`
// 6pD9FNZ9U5LNbhzjaafhgPBbsYux34xfg9zYve4QiT7o
// 2039280
// Config {
//     json_rpc_url: "https://devnet.solana.com",
//     websocket_url: "wss://devnet.solana.com",
//     keypair_path: "C:\\Users\\dz\\.config\\solana\\id.json",
//     address_labels: {
//         "11111111111111111111111111111111": "System Program",
//     },
//     commitment: "confirmed",
// }
// "3dw8u4bNfahDBuei1DUE3Sq6uMJ8EGJFBRbRxcJJ2X49fvzZTTUE6pEFexMe6vDbyMojSeZemUUgpNxCtU8VWwCC"
// Create mint token "3HJhjH7Z2n3uELk8hd5fM6nDzd2tNQh9uT67VC39KKYD75ZErmQgjr2UjWX5HJhEpQ4nyDyArSSFSAt3XajfXSuk"
// Mint  CMjGiZvJoz8E2HdWJnHN62gMyaYgNUCXzpsWsDLtBZTM
// Create associated token account"4pqrhvYTaSZKNq2TmTWvG3XjR5oTBZPjjboBTc69WPUhpdJSdJzSri2aAWcGKi6Kd8ECdRN87NAUH1ESkzR8LoQn"
// My token account Hh37xMMHPJJBL1gWPj5G66obYLKqth53DGagJUH4Wiz4
// Create mint 42"KEU8D6YLFNTWU5c24DkQhnWLzzgw6fm59LnptnSMLYLzRp9RR7kqrtiMvSYJFfL2npSnenb8QY3vFcJovTjEBx5"



use std::{convert::TryFrom, error::Error, fs::File, io::Read};

use solana_client::{blockhash_query::BlockhashQuery, rpc_client::RpcClient};
use solana_program::{
    instruction::{AccountMeta, Instruction},
    message::Message,
    program_error::ProgramError,
    program_option::COption,
    pubkey::Pubkey,
    system_instruction, sysvar,
};
use solana_sdk::program_pack::*;
use solana_sdk::{
    account::Account,
    signature::{Keypair, Presigner, Signature, Signer},
    signers::Signers,
    transaction::Transaction,
};
use spl_token::{instruction::TokenInstruction, state::Mint};

solana_program::declare_id!("TokenkegQfeZyiNwAJbNbGKPFXCWuBvf9Ss623VQ5DA");

// sol token accounts
mod native_mint {
    solana_program::declare_id!("So11111111111111111111111111111111111111112");
}

fn main() -> std::result::Result<(), Box<dyn Error>> {
    a()?;
    b()?;
    Ok(())
}

fn a() -> std::result::Result<(), Box<dyn Error>> {
    // NOTE: could use dirs crate as solana cli does to get path, but doing just what is doing
    let config = solana_cli_config::Config::load("C:/Users/dz/.config/solana/cli/config.yml")?;
    // NOTE: assuming file storage
    let mut key = std::fs::File::open(&config.keypair_path)?;
    let mut buf = Vec::new();
    key.read_to_end(&mut buf)?;
    let key_value = serde_json::de::from_slice::<Vec<u8>>(&buf)?;
    let fee_payer = Keypair::from_bytes(&key_value)?;
    let fee_payer2 = Keypair::from_bytes(&key_value)?;
    let new_account = Keypair::new();
    let rpc_client = RpcClient::new(config.json_rpc_url.clone());
    let minimum_balance_for_rent_exemption =
        rpc_client.get_minimum_balance_for_rent_exemption(165)?;

    // TODO: env logger
    println!("{:#?}", new_account.pubkey());
    println!("{:#?}", minimum_balance_for_rent_exemption);
    println!("{:#?}", config);

    // create account for native mi
    let instructions = vec![
        system_instruction::create_account(
            &fee_payer.pubkey().clone(),
            &new_account.pubkey(), //&account,
            minimum_balance_for_rent_exemption,
            165, //Account::LEN as u64,
            &spl_token::id(),
            // not found spec for initialize_account for system_program data, if that is needed for system at all?
            //&solana_program::system_program::id(),
        ),
        initialize_account(
            //&solana_program::system_program::id(),
            &spl_token::id(),
            &new_account.pubkey(),
            &native_mint::id(),
            &new_account.pubkey(),
        )?,
    ];

    let create_account_message = Message::new(&instructions, Some(&fee_payer.pubkey().clone()));
    let blockhash_query = BlockhashQuery::default();
    let (recent_blockhash, fee_calculator) =
        blockhash_query.get_blockhash_and_fee_calculator(&rpc_client, rpc_client.commitment())?;
    let signers: Vec<Box<dyn Signer>> = vec![Box::new(fee_payer), Box::new(new_account)];

    let mut create_account_transaction = Transaction::new_unsigned(create_account_message);

    create_account_transaction.try_sign(
        &signers, //&signer_info.signers,
        recent_blockhash,
    )?;
    let result =
        rpc_client.send_and_confirm_transaction_with_spinner(&create_account_transaction)?;
    println!("{:#?}", result.to_string());
    Ok(())
}

fn b() -> std::result::Result<(), Box<dyn Error>> {
    // NOTE: could use dirs crate as solana cli does to get path, but doing just what is doing
    let config = solana_cli_config::Config::load("C:/Users/dz/.config/solana/cli/config.yml")?;
    // NOTE: assuming file storage
    let mut key = std::fs::File::open(&config.keypair_path)?;
    let mut buf = Vec::new();
    key.read_to_end(&mut buf)?;
    let key_value = serde_json::de::from_slice::<Vec<u8>>(&buf)?;
    let fee_payer = Keypair::from_bytes(&key_value)?;
    let fee_payer2 = Keypair::from_bytes(&key_value)?;
    let new_account = Keypair::new();
    let rpc_client = RpcClient::new(config.json_rpc_url.clone());
    let minimum_balance_for_rent_exemption =
        rpc_client.get_minimum_balance_for_rent_exemption(165)?;

    let minimum_balance_for_rent_exemption =
        rpc_client.get_minimum_balance_for_rent_exemption(Mint::LEN)?;

    let token_key_pair = Keypair::new();
    let instructions = vec![
        system_instruction::create_account(
            &fee_payer2.pubkey().clone(),
            &token_key_pair.pubkey(),
            minimum_balance_for_rent_exemption,
            Mint::LEN as u64,
            &spl_token::id(),
        ),
        initialize_mint(
            &spl_token::id(),
            &token_key_pair.pubkey(),
            &fee_payer2.pubkey(),
            6,
        )?,
    ];

    let mint = token_key_pair.pubkey().clone();

    let token_key_bytes = token_key_pair.to_base58_string();

    // why keypair has no clone? (its fields do have)
    let token_key_pair = Keypair::from_base58_string(&token_key_bytes);
    let token_key_pair2 = Keypair::from_base58_string(&token_key_bytes);

    assert_eq!(token_key_pair.pubkey(), token_key_pair2.pubkey());
    assert_eq!(
        token_key_pair.secret().to_bytes(),
        token_key_pair2.secret().to_bytes()
    );

    let create_account_message = Message::new(&instructions, Some(&fee_payer2.pubkey()));
    let blockhash_query = BlockhashQuery::default();
    let (recent_blockhash, fee_calculator) =
        blockhash_query.get_blockhash_and_fee_calculator(&rpc_client, rpc_client.commitment())?;
    let signers: Vec<Box<dyn Signer>> = vec![Box::new(fee_payer2), Box::new(token_key_pair)];

    let mut create_account_transaction = Transaction::new_unsigned(create_account_message);

    create_account_transaction.try_sign(&signers, recent_blockhash)?;
    let result =
        rpc_client.send_and_confirm_transaction_with_spinner(&create_account_transaction)?;
    println!("Create mint token {:#?}", result.to_string());
    println!("Mint  {:#?}", &mint);

    let instructions = vec![create_associated_token_account(
        &fee_payer.pubkey(),
        &fee_payer.pubkey(),
        &mint,
    )];

    let account = get_associated_token_address(&fee_payer.pubkey(), &mint);

    let create_account_message = Message::new(&instructions, Some(&fee_payer.pubkey()));
    let blockhash_query = BlockhashQuery::default();
    let (recent_blockhash, fee_calculator) =
        blockhash_query.get_blockhash_and_fee_calculator(&rpc_client, rpc_client.commitment())?;
    let signers: Vec<Box<dyn Signer>> = vec![Box::new(fee_payer) as Box<dyn Signer>];

    let mut create_account_transaction = Transaction::new_unsigned(create_account_message);
    create_account_transaction.try_sign(&signers, recent_blockhash)?;

    let result =
        rpc_client.send_and_confirm_transaction_with_spinner(&create_account_transaction)?;
    println!("Create associated token account{:#?}", result.to_string());

    println!("My token account {:#?}", account);

    let fee_payer = Keypair::from_bytes(&key_value)?;
    let instructions = vec![
        mint_to_checked(
            &spl_token::id(),
            &mint,
            &account,
            &fee_payer.pubkey(),
        )?
    ];

    let mint_message = Message::new(&instructions, Some(&fee_payer.pubkey()));
    let blockhash_query = BlockhashQuery::default();
    let (recent_blockhash, fee_calculator) =
        blockhash_query.get_blockhash_and_fee_calculator(&rpc_client, rpc_client.commitment())?;
    let signers: Vec<Box<dyn Signer>> = vec![Box::new(fee_payer) as Box<dyn Signer>];

    let mut mint_transaction = Transaction::new_unsigned(mint_message);
    mint_transaction.try_sign(&signers, recent_blockhash)?;

    let result =
        rpc_client.send_and_confirm_transaction_with_spinner(&mint_transaction)?;
    println!("Create mint 42{:#?}", result.to_string());


    Ok(())
}

pub fn create_associated_token_account(
    funding_address: &Pubkey,
    wallet_address: &Pubkey,
    spl_token_mint_address: &Pubkey,
) -> Instruction {
    let associated_account_address =
        get_associated_token_address(wallet_address, spl_token_mint_address);

    Instruction {
        program_id: associated_token::id(),
        accounts: vec![
            AccountMeta::new(*funding_address, true),
            AccountMeta::new(associated_account_address, false),
            AccountMeta::new_readonly(*wallet_address, false),
            AccountMeta::new_readonly(*spl_token_mint_address, false),
            AccountMeta::new_readonly(solana_program::system_program::id(), false),
            AccountMeta::new_readonly(spl_token::id(), false),
            AccountMeta::new_readonly(sysvar::rent::id(), false),
        ],
        data: vec![],
    }
}

/// Creates a `MintToChecked` instruction.
pub fn mint_to_checked(
    token_program_id: &Pubkey,
    mint_pubkey: &Pubkey,
    account_pubkey: &Pubkey,
    owner_pubkey: &Pubkey,
) -> Result<Instruction, ProgramError> {
    let data = TokenInstruction::MintToChecked { amount: 42, decimals: 6 }.pack();

    let mut accounts = Vec::with_capacity(3);
    accounts.push(AccountMeta::new(*mint_pubkey, false));
    accounts.push(AccountMeta::new(*account_pubkey, false));
    accounts.push(AccountMeta::new_readonly(
        *owner_pubkey,
        true,
    ));

    Ok(Instruction {
        program_id: *token_program_id,
        accounts,
        data,
    })
}

mod associated_token {
    solana_program::declare_id!("ATokenGPvbdGVxr1b2hvZbsiqW5xWH25efTNsLJA8knL");
}

pub fn get_associated_token_address(
    wallet_address: &Pubkey,
    spl_token_mint_address: &Pubkey,
) -> Pubkey {
    get_associated_token_address_and_bump_seed(
        &wallet_address,
        &spl_token_mint_address,
        &associated_token::id(),
    )
    .0
}

pub fn get_associated_token_address_and_bump_seed(
    wallet_address: &Pubkey,
    spl_token_mint_address: &Pubkey,
    program_id: &Pubkey,
) -> (Pubkey, u8) {
    Pubkey::find_program_address(
        &[
            &wallet_address.to_bytes(),
            &spl_token::id().to_bytes(),
            &spl_token_mint_address.to_bytes(),
        ],
        program_id,
    )
}

pub fn initialize_account(
    token_program_id: &Pubkey,
    account_pubkey: &Pubkey,
    mint_pubkey: &Pubkey,
    owner_pubkey: &Pubkey,
) -> Result<Instruction, ProgramError> {
    let data = spl_token::instruction::TokenInstruction::InitializeAccount.pack();

    let accounts = vec![
        AccountMeta::new(*account_pubkey, false),
        AccountMeta::new_readonly(*mint_pubkey, false),
        AccountMeta::new_readonly(*owner_pubkey, false),
        AccountMeta::new_readonly(sysvar::rent::id(), false),
    ];

    Ok(Instruction {
        program_id: *token_program_id,
        accounts,
        data,
    })
}

/// Creates a `InitializeMint` instruction.
pub fn initialize_mint(
    token_program_id: &Pubkey,
    mint_pubkey: &Pubkey,
    mint_authority_pubkey: &Pubkey,
    decimals: u8,
) -> Result<Instruction, ProgramError> {
    let data = TokenInstruction::InitializeMint {
        mint_authority: *mint_authority_pubkey,
        freeze_authority: COption::None,
        decimals,
    }
    .pack();

    let accounts = vec![
        AccountMeta::new(*mint_pubkey, false),
        AccountMeta::new_readonly(sysvar::rent::id(), false),
    ];

    Ok(Instruction {
        program_id: *token_program_id,
        accounts,
        data,
    })
}
