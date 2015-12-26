#load "model.fsx"

// with all roles and meta role to get meta info
type Admin = interface Role with member id = 3
//type reflect_operation 
type Auditor = interface Role with member id = 4


type Health = Good | Ill
type Sex = Male | Female
type Year = int


type Resource = 
  member val health = Health.Good 
  member val sex = Sex.Male
  member val year = 0


//for different contry one wants only males other all type Rule. max born year limit