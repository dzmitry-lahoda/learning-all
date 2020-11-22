#[derive(Debug)]
pub struct HighScores<'a> {
    scores:&'a [u32] // lifetime of scores is propagted from caller via `'a`
}
impl<'a> HighScores<'a> {
    pub fn new(scores: &'a [u32]) -> Self {
        HighScores{scores:scores}        
    }   

    pub fn scores(&self) -> &[u32] {
        self.scores
    }

    pub fn latest(&self) -> Option<u32> {
        self.scores.last().copied()
    }

    pub fn personal_best(&self) -> Option<u32> {
        self.scores.iter().fold(None, |max, next| match max {
            None => Some(next.clone()),
            Some(prev) => Some(if &prev < next {next.clone()} else {prev})
        })
    }

    pub fn personal_top_three(&self) -> Vec<u32> {
        let mut result = self.scores.to_vec();
        result.sort_unstable_by(|a, b| a.cmp(b).reverse());
        result.truncate(3);
        result
    }
}
