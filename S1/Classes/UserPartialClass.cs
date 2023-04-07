namespace S1.Model;

public partial class User
{
    public string GetFullName
    {
        get
        {
            return $"{SecondName} {FirstName} {ThirdName}";
        }
    }
}