using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LazyVocabulary.Web.Models
{
    public class OverviewProfileViewModel
    {
        public string OwnerId { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string DateOfBirth { get; set; }

        public string CreatedAt { get; set; }

        public string AvatarImagePath { get; set; }

        public string UserName { get; set; }

        public int DictionariesCount { get; set; }

        public int SubscribersCount { get; set; }

        public int SubscriptionsCount { get; set; }
    }
}