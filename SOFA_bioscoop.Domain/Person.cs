namespace SOFA_bioscoop.Domain
{
    public class Person
    {
        public string Name { get; set; }
        public Role Role { get; set; }

        public Person(string name, Role role)
        {
            Name = name;
            Role = role;
        }
    }
}
