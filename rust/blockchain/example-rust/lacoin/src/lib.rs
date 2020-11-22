extern crate chrono;
extern crate sha3;
extern crate bincode;

#[cfg(test)]
mod tests {
    
    use chrono::prelude::*;    
    use sha3::{Digest, Sha3_512};
    use bincode;
    
    // TODO: split into strongly typed structs as in memory vs structs as read

    #[derive(Debug)]
    struct HashBaseV1{
        index : u64,
        timestamp : DateTime<Utc>,
        // TODO: make this u8 of limited size
        data : String,
        // TODO: make size of hash to be generic parameter to allow tesing PoW 
        // TODO: write test for PoW and Nonce (so few bit Nonce allow run in unit test)
        // TODO: make it GenericArray<u8, Self::OutputSize>
        previous_hash : Vec<u8>,        
    }

    #[derive(Debug)]
    struct BlockV1 {
        hash_base: HashBaseV1,
        // TODO: make it GenericArray<u8, Self::OutputSize>
        hash: Vec<u8>,
    }

    fn hash_block<'a>(genesisBase:&HashBaseV1) -> Vec<u8> {
        let mut hasher = Sha3_512::new();
        // TOOD: use Serde
        hasher.input(genesisBase.index.to_be_bytes());
        let encodedTimeStamp = bincode::serialize(&genesisBase.timestamp).unwrap();
        hasher.input(encodedTimeStamp);
        hasher.input(&genesisBase.data);
        hasher.input(&genesisBase.previous_hash);
        hasher.result().to_vec()
    }

    fn next_block(b:&BlockV1) -> BlockV1 {
        let nextIndex = b.hash_base.index + 1;
        let data = format!("Next block {}", nextIndex);
        let genesisBase = HashBaseV1 { index: nextIndex, timestamp: Utc::now(), data: data, previous_hash: b.hash.clone()};
        let binaryHash = hash_block(&genesisBase);
        BlockV1 { hash_base : genesisBase, hash : binaryHash}
    }

    #[test]
    fn it_works() {
        let genesisHash = "0".as_bytes().to_owned();
        let genesisBase = HashBaseV1 { index: 0u64, timestamp: Utc::now(), data: "Generis Block".to_owned(), previous_hash: genesisHash};

        let binaryHash = hash_block(&genesisBase);
        
        //let hash = String::from_utf8(binaryHash);
        let genesis = BlockV1 { hash_base : genesisBase, hash : binaryHash};
        let second = next_block(&genesis);

        assert_eq!(second.hash_base.previous_hash, genesis.hash);
    }
}
