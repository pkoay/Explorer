using System.Collections.Generic;

namespace WalzExplorer
{
    /// <summary>
    /// A simple data transfer object (DTO) that contains raw data about a Node.
    /// </summary>
    public class Person
    {
        public string PersonID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Login { get; set; }
    }
}