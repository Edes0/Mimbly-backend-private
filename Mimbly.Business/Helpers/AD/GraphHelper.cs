﻿namespace Mimbly.Business.Helpers.AD;

using Domain.Entities.AD;
using Interfaces.AD;
using Microsoft.Graph;

public class GraphHelper : IGraphHelper
{
    private readonly IGraphService _graphService;

    public GraphHelper(IGraphService graphService)
    {
        _graphService = graphService;
    }

    public Invitation GetInvitation(AdUser user, string redirectUrl)
    {
        var invite = new Invitation
        {
            InvitedUserDisplayName = user.DisplayName,
            InvitedUserEmailAddress = user.Email,
            InviteRedirectUrl = redirectUrl,
            SendInvitationMessage = true,
            InvitedUserMessageInfo = new InvitedUserMessageInfo { MessageLanguage = "sv-SE" }
        };

        return invite;
    }

    public User GetUserInfo(AdUser user)
    {
        var userInfo = new User
        {
            JobTitle = user.JobTitle,
            MobilePhone = user.Phone,
            StreetAddress = user.StreetAddress,
            City = user.City,
            Country = user.Country
        };

        return userInfo;
    }

    public async Task<string?> InviteAndGetUserId(Invitation invite)
    {
        var client = _graphService.GetClient();

        var resp = await client.Invitations.Request().AddAsync(invite);
        var userId = resp.InvitedUser.Id;

        return userId;
    }

    public async void UpdateUserInfo(User userInfo, string userId)
    {
        var client = _graphService.GetClient();
        try
        {
            await client.Users[userId].Request().UpdateAsync(userInfo);
        }
        catch (Exception ex)
        {

        }
    }

    public async void AddMemberToGroup(string groupId, string userId)
    {
        var client = _graphService.GetClient();

        var dirObj = new DirectoryObject { Id = userId };

        try
        {
            await client.Groups[groupId].Members.References.Request().AddAsync(dirObj);
        }
        catch (ServiceException ex)
        {

        }
    }

    public async void AddOwnerToGroup(string groupId, string userId)
    {
        var client = _graphService.GetClient();

        var dirObj = new DirectoryObject { Id = userId };
        await client.Groups[groupId].Owners.References.Request().AddAsync(dirObj);
    }

}
