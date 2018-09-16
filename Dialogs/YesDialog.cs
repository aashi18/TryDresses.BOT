using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;


namespace TryDresses.Dialogs
{
    [Serializable]
    public class YesDialog : IDialog<object>
    {
        public async Task StartAsync(IDialogContext context)
        {
            await context.PostAsync("Okay. I need to see you. Please upload your photo");
            context.Call(new UploadDialog(), this.ResumeAfterOptionDialog);

        }
        private async Task ResumeAfterOptionDialog(IDialogContext context, IAwaitable<object> result)
        {

        }
    }
}