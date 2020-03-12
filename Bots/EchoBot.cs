// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.
//
// Generated with Bot Builder V4 SDK Template for Visual Studio EchoBot v4.6.2
using Microsoft.Azure.WebJobs.Host;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
using AdaptiveCards;
using System.IO;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using System;

namespace wecanchat.Bots
{
    public class EchoBot : ActivityHandler
    {
        protected override async Task OnMessageActivityAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            var replyText = $"Echo: {turnContext.Activity.Text}";
            await turnContext.SendActivityAsync(MessageFactory.Text(replyText, replyText), cancellationToken);
            //card.CreateAdaptiveCardAttachment();
        }

        protected override async Task OnMembersAddedAsync(IList<ChannelAccount> membersAdded, ITurnContext<IConversationUpdateActivity> turnContext, CancellationToken cancellationToken)
        {
            var welcomeText = "Hello and welcome!";
            foreach (var member in membersAdded)
            {
                if (member.Id != turnContext.Activity.Recipient.Id)
                {
                    //await turnContext.SendActivityAsync(MessageFactory.Text(welcomeText, welcomeText), speak: "This is spoken", cancellationToken);
                    await turnContext.SendActivityAsync("This is displayed", speak: "This is spoken", inputHint: "expectingInput");
                    await SendIntroCardAsync(turnContext, cancellationToken);
                }
            }
           
            // await turnContext.SendActivityAsync(CreateAdaptiveCardUsingSdk()

        }
        private  async Task SendIntroCardAsync(ITurnContext turnContext, CancellationToken cancellationToken)
        {

            var response = turnContext.Activity.CreateReply();
            response.Attachments = new List<Attachment>() { CreateAdaptiveCardUsingSdk() };

         //   var response = MessageFactory.Attachment(attachment: card.ToAttachment());
            await turnContext.SendActivityAsync(response, cancellationToken);
        }
        private Attachment CreateAdaptiveCardUsingSdk()
        {
            var card = new AdaptiveCard();
            card.Body.Add(new AdaptiveTextBlock() { Text = "Colour", Size = AdaptiveTextSize.Medium, Weight = AdaptiveTextWeight.Bolder });
            card.Body.Add(new AdaptiveChoiceSetInput()
            {
                Id = "Colour",
                Style = AdaptiveChoiceInputStyle.Compact,
                Choices = new List<AdaptiveChoice>(new[] {
                        new AdaptiveChoice() { Title = "Red", Value = "RED" },
                        new AdaptiveChoice() { Title = "Green", Value = "GREEN" },
                        new AdaptiveChoice() { Title = "Blue", Value = "BLUE" } })
            });
            card.Body.Add(new AdaptiveTextBlock() { Text = "Registration number:", Size = AdaptiveTextSize.Medium, Weight = AdaptiveTextWeight.Bolder });
            card.Body.Add(new AdaptiveTextInput() { Style = AdaptiveTextInputStyle.Text, Id = "RegistrationNumber" });
            card.Actions.Add(new AdaptiveSubmitAction() { Title = "Submit" });
            return new Attachment()
            {
                ContentType = AdaptiveCard.ContentType,
                Content = card
            };
        }


      

      
        
    }

}
