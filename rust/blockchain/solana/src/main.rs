
// what it does
// 1. uses exiting account with money (i did not read it out of config, just plugged into code)
// 2. generates new non registered account
// 3. sends new account creations instructions with payment 
// 4. example https://explorer.solana.com/tx/3Y6QoDVJgm7rnuHKzmohgT5SnbMqmmY9fxSABJqK77xRCGc6Hvf4kVMBVXTMAEdHZzvcnErB1e9snnjGBRPmtYFG?cluster=devnet
// 5. code is shit, i did not clippied it:)

use std::{convert::TryFrom, error::Error};

use solana_client::{blockhash_query::BlockhashQuery, rpc_client::RpcClient};
use solana_program::{
    instruction::{AccountMeta, Instruction},
    message::Message,
    program_error::ProgramError,
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


solana_program::declare_id!("TokenkegQfeZyiNwAJbNbGKPFXCWuBvf9Ss623VQ5DA");

fn main() -> std::result::Result<(), Box<dyn Error>> {
    let config = solana_cli_config::Config::load("C:/Users/dz/.config/solana/cli/config.yml")?;
    // TODO: i just plugged my test key here
    // 1. read wallet from config
    // 2. read private key from config
    let fee_payer = Keypair::from_bytes(&[
        14, 224, 178, 47, 249, 124, 214, 7, 30, 215, 60, 72, 132, 185, 33, 18, 100, 2, 75, 239, 65,
        42, 142, 40, 30, 39, 206, 42, 199, 123, 80, 231, 151, 214, 31, 152, 136, 191, 193, 206, 32,
        82, 12, 32, 221, 108, 49, 134, 82, 146, 253, 60, 68, 164, 214, 137, 132, 18, 172, 59, 68,
        113, 177, 180,
    ])?;
    let keypair = Keypair::new();
    let rpc_client = RpcClient::new(config.json_rpc_url.clone());
    let minimum_balance_for_rent_exemption =
        rpc_client.get_minimum_balance_for_rent_exemption(165)?;

    // TODO: env logger
    println!("{:#?}", keypair.pubkey());
    println!("{:#?}", minimum_balance_for_rent_exemption);
    println!("{:#?}", config);

    let instructions = vec![
        system_instruction::create_account(
            &fee_payer.pubkey(),
            &keypair.pubkey(), //&account,
            minimum_balance_for_rent_exemption,
            165,   //Account::LEN as u64,
            &id(), //&spl_token::id(),
        ),
        // TODO: create token account
        // initialize_account(
        //     &id(),//&spl_token::id(),
        //     &keypair.pubkey(),//&account,
        //     &keypair.pubkey(),//&token,
        //     &keypair.pubkey(), //&config.owner
        // )?,
    ];

    let message = Message::new(&instructions, Some(&fee_payer.pubkey()));
    let blockhash_query = BlockhashQuery::default();
    let (recent_blockhash, fee_calculator) =
        blockhash_query.get_blockhash_and_fee_calculator(&rpc_client, rpc_client.commitment())?;
    
    //// TODO: find official way to have signers
    let signers = MySigners {
        keypair1: fee_payer,
        keypair2: keypair,
    };

    let mut transaction = Transaction::new_unsigned(message);

    transaction.try_sign(
        &signers, //&signer_info.signers,
        recent_blockhash,
    )?;
    let result = rpc_client.send_and_confirm_transaction_with_spinner(&transaction)?;
    
    println!("{:#?}", result.to_string());
    Ok(())
}

struct MySigners {
    pub keypair1: Keypair,
    pub keypair2: Keypair,
}

impl Signers for MySigners {
    fn pubkeys(&self) -> Vec<Pubkey> {
        vec![self.keypair1.pubkey(), self.keypair2.pubkey()]
    }

    fn try_pubkeys(&self) -> Result<Vec<Pubkey>, solana_sdk::signature::SignerError> {
        Ok(vec![self.keypair1.pubkey(), self.keypair2.pubkey()])
    }

    fn sign_message(&self, message: &[u8]) -> Vec<Signature> {
        vec![
            self.keypair1.sign_message(message),
            self.keypair2.sign_message(message),
        ]
    }

    fn try_sign_message(
        &self,
        message: &[u8],
    ) -> Result<Vec<Signature>, solana_sdk::signature::SignerError> {
        Ok(vec![
            self.keypair1.sign_message(message),
            self.keypair2.sign_message(message),
        ])
    }
}

// note: just copy paste from cli, not used yet as that is step 4 (advanced)
// may be will do on tomorrow
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
