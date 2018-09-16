using System;
using System.Collections.Generic;
using System.Linq;                                                          //Sleep
using System.Threading.Tasks;                                               //Typing show
using System.Web;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System.Net.Http;



namespace TryDresses.Dialogs
{
    [Serializable]
    public class UploadDialog : IDialog<object>
    {
        public async Task StartAsync(IDialogContext context)
        {

            context.Wait(this.MessageReceivedAsync);

        }
        public virtual async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var activity = await result;

            if (activity.Attachments != null && activity.Attachments.Any())
            {
                await context.PostAsync("Thank You. Let me analyze your face.");
                var attachmentUrl = new Uri(activity.Attachments[0].ContentUrl);

                var name = activity.Attachments[0].Name;
                System.IO.File.WriteAllText(@"C:\Users\mtcuser\Documents\VITON-master\data\viton_test_pairs.txt", string.Empty);
                using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(@"C:\Users\mtcuser\Documents\VITON-master\data\viton_test_pairs.txt", true))
                {
                    file.Write(name);
                }


                

                // Image save to pass in faceswap

             
                this.ShowOptions(context);
                
            }



        }

        private async Task ResumeAfterOptionDialog(IDialogContext context, IAwaitable<object> result)
        {

        }
        private void ShowOptions(IDialogContext context)
        {
            PromptDialog.Choice(context, OnOptionSelected, new List<string>() { "Yes", "No" }, "Do you have photo of dress which you want to try?", "Not a valid option", 3);
        }

        private async Task OnOptionSelected(IDialogContext context, IAwaitable<string> result)
        {
            try
            {
                string optionSelected = await result;

                switch (optionSelected)
                {
                    case "Yes":
                        context.Call(new DressDialog(), this.ResumeAfterOptionDialog);
                        break;

                    case "No":
                        context.Call(new NoDressDialog(), this.ResumeAfterOptionDialog);
                        break;
                }
            }
            catch (TooManyAttemptsException ex)
            {
                await context.PostAsync($"Ooops! Too many attempts :(. But don't worry, I'm handling that exception and you can try again!");

                context.Wait(this.MessageReceivedAsync);
            }
        }
    }

}
    