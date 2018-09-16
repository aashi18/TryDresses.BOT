using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using Microsoft.Bot.Connector;


namespace TryDresses.Dialogs
{
    [Serializable]
    public class DressDialog : IDialog<object>
    {
        public async Task StartAsync(IDialogContext context)
        {

            await context.PostAsync("Okay. Please upload picture of your dress");
            context.Wait(this.MessageReceivedAsync);

        }
        public virtual async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
        {

            var activity = await result;

            if (activity.Attachments != null && activity.Attachments.Any())
            {

                var attachmentUrl = new Uri(activity.Attachments[0].ContentUrl);

                var name = activity.Attachments[0].Name;
                //await context.PostAsync($"{name}");
                using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(@"C:\Users\mtcuser\Documents\VITON-master\data\viton_test_pairs.txt", true))
                {
                    file.Write(" " + name);
                }
                await context.PostAsync($"Please wait for the result.");
                context.Call(new VitonDialog(), this.ResumeAfterOptionDialog);


            }
            else
            {
                await context.PostAsync($"I can only take photos. Please upload your dress photo.");
            }
        }
        private async Task ResumeAfterOptionDialog(IDialogContext context, IAwaitable<object> result)
        {

        }
    }
}