using Data.Data;
using Data.DatabaseModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.DataAccess
{
    class UserStoryDataAccess
    {
        public UserStory AddUserStory(UserStory story)
        {
            using (var context = new ThemisContext())
            {
                context.UserStories.Add(story);
                context.SaveChanges();
            }
            return story;
        }
        public UserStory[] GetUserStoriesForProject(string projectName)
        {
            using (var context = new ThemisContext())
            {
                var userStories = context.UserStories.Where(u => u.Name.Equals(projectName)).ToArray();
                return userStories;
            }
        }
        public UserStory[] EditDescription(string projectName, string oldDescription, string newDescription)
        {
            using (var context = new ThemisContext())
            {
                var userStory = context.UserStories.Where(u => u.Name.Equals(projectName) && u.Description.Equals(oldDescription)).FirstOrDefault();
                if (userStory != null)
                {
                    userStory.Description = newDescription;
                }
                context.Entry(userStory).State = EntityState.Modified;
                context.SaveChanges();
                return GetUserStoriesForProject(projectName);
            }
        }
    }
}
