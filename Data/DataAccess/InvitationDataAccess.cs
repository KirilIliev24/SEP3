using Data.Data;
using Data.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.DataAccess
{
    class InvitationDataAccess
    {
        public User AddInvitation(string projectName, string invitedUser)
        {
            using (var context = new ThemisContext())
            {
                var invitation = new InvitationModel()
                {
                    Name = projectName,
                    username = invitedUser
                };
                context.Invitation.Add(invitation);
                context.SaveChanges();
                return context.Users.Find(invitedUser);
            }
        }
        public string[] GetInvitationsByUser(string InvitedUser)
        {
            using (var context = new ThemisContext())
            {
                var invitations = context.Invitation.Where(i => i.username.Equals(InvitedUser)).Select(i => i.Name).ToArray();
                return invitations;
            }
        }
        public User DeleteInvitation(String projectName, String invitedUser)
        {
            using (var context = new ThemisContext())
            {
                var declinedInvitation = context.Invitation.Where(i => i.username.Equals(invitedUser) && i.Name.Equals(projectName));
                context.Invitation.Remove(declinedInvitation.FirstOrDefault());
                context.SaveChanges();
                return context.Users.Find(invitedUser);
            }
        }
        public Project AcceptInvitation(String projectName, String invitedUser)
        {
            using (var context = new ThemisContext())
            {
                var user = context.Users.Find(invitedUser);
                var AcceptedInvitation = context.Invitation.Where(i => i.username.Equals(invitedUser) && i.Name.Equals(projectName));
                var workGroupId = context.WorkGroups.Where(w => w.Name.Equals(projectName)).Select(w => w.WorkGroupId).FirstOrDefault();
                context.WorkGroupDevelopers.Add(new WorkGroupDeveloper()
                {
                    WorkGroupId = workGroupId,
                    Developer = user
                });
                context.Invitation.Remove(AcceptedInvitation.FirstOrDefault());
                context.SaveChanges();
                return context.Projects.Find(projectName);
            }
        }
    }
}
