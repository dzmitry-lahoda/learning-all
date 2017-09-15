

struct User 
{
   name : String,
   session_count : i64,
   active:bool   
}

fn build_user(name:String, session_count:i64) -> User
{
    User { name, session_count, active: true }
}

fn main()
{
  let mut user = User
  {
      name : String::from("John"),
      session_count : 100,
      active : true
  };
  user.active = false;

  let user2 = User { active : true, ..user};
}