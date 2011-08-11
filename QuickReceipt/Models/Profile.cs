using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace QuickReceipt.Models
{
    public class Profile
    {
        public int Id { get; set; }

        [Required(ErrorMessage="A Profile Name is required!")]
        public string Name { get; set; }

        public Category Category { get; set; }

        public int CategoryId
        {
            get
            {
                return (Category == null ? 0 : Category.Id);
            }
            set
            {
                if (Category == null)
                    Category = new Category();
                Category.Id = value;
            }
        }

        public ProfileType Type { get; set; }

        public int TypeId
        {
            get
            {
                return (Type == null ? 0 : Type.Id);
            }
            set
            {
                if (Type == null)
                    Type = new ProfileType();
                Type.Id = value;
            }
        }

        public Item Item { get; set; }

        public int ItemId
        {
            get
            {
                return (Item == null ? 0 : Item.Id);
            }
            set
            {
                if (Item == null)
                    Item = new Item();
                Item.Id = value;
            }
        }

        public Issue Issue { get; set; }

        public int IssueId
        {
            get
            {
                return (Issue == null ? 0 : Issue.Id);
            }
            set
            {
                if (Issue == null)
                    Issue = new Issue();
                Issue.Id = value;
            }
        }
    }
}