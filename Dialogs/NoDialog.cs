using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

    
namespace TryDresses.Dialogs
{
    [Serializable]
    public class NoDialog : IDialog<object>
    {
        public async Task StartAsync(IDialogContext context)
        {
            await context.PostAsync("Happy meeting you. Let me help you next time. Have a good day.");
            context.Call(new RootDialog(), this.ResumeAfterOptionDialog);

        }
        private async Task ResumeAfterOptionDialog(IDialogContext context, IAwaitable<object> result)
        {

        }
    }
}