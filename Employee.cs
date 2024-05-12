namespace CatWorx.BadgeMaker
{
    class Employee
    {
       private string CoName; 
       private string FirstName;
       private string LastName;
       private int Id;
       private string PhotoUrl;
       public Employee(string coName, string firstName, string lastName, int id, string photoUrl){
        CoName = coName;
        FirstName = firstName;
        LastName = lastName;
        Id = id;
        PhotoUrl = photoUrl;
       }
        public string GetFullName() {
            return FirstName + " " + LastName;
        }
        public int GetId() {
            return Id;
        }
        public string GetPhotoUrl() {
            return PhotoUrl;
        }
        public string GetCompanyName() {
            return CoName;
        }
    }
}