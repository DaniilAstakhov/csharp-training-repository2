using System;
using LinqToDB.Mapping;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAddressbookTests
{
    [Table(Name = "group_list")]
    public class GroupData : IEquatable<GroupData>, IComparable<GroupData>
    {
        private string name;
        private string header = "";
        private string footer = "";

        public GroupData()
        {
        }
        public GroupData(string name) //конструктор только для имени
        {
            this.name = name;
        }

        public GroupData(string name,string header, string footer) //конструктор для имени хэдэра и футера
        {
            this.name = name; 
            this.header = header;
            this.footer = footer;
        }

        public bool Equals(GroupData other)
        {
            if(Object.ReferenceEquals(other, null))
                return false;

            if(Object.ReferenceEquals(this, other)) 
                return true;

            return Name == other.Name;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        public override string ToString()
        {
            return "name=" + Name + "\nheader= " + Header + "\nfooter= " + Footer;
        }

        public int CompareTo(GroupData other)
        {
            if (Object.ReferenceEquals(other, null))            
                return 1;  
            
            return Name.CompareTo(other.Name);
        }

        [Column(Name = "group_name"), NotNull]
        public string Name { get { return name; } set { name = value; } }

        [Column(Name = "group_header"), NotNull]
        public string Header { get { return header; } set { header = value; } }

        [Column(Name = "group_footer"), NotNull]
        public string Footer { get { return footer; } set { footer = value; } }

        [Column(Name = "group_id"), PrimaryKey, Identity]
        public string Id { get; set; }

        public static List<GroupData> GetAll()
        {
            using (AddressBookDB db = new AddressBookDB())
            {
                return (from g in db.Groups select g).ToList();
            }
        }

        public List<ContactData> GetContacts()
        {
            using (AddressBookDB db = new AddressBookDB())
            {
                return (from c in db.Contacts
                        from gcr in db.GCR.Where(p => p.GroupId == Id && p.ContactId == c.id && c.Deprecated == "0000-00-00 00:00:00")
                        select c).Distinct().ToList();
            }
        }
        public static List<GroupData> GetGroupWithContactsInIt()
        {
            using (AddressBookDB db = new AddressBookDB())
            {
                return (from g in db.Groups
                    from gcr in db.GCR.Where(p => p.GroupId == g.Id)
                        select g).ToList();
            }
        }
    }
}
