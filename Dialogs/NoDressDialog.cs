using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using Microsoft.Bot.Connector;


namespace TryDresses.Dialogs
{
    [Serializable]
    public class NoDressDialog : IDialog<object>
    {
        public async Task StartAsync(IDialogContext context)
        {
            await context.PostAsync("Okay. I will find myself.");

            var a = File();
            //await context.PostAsync(a);
            context.Call(new RootDialog(), this.ResumeAfterOptionDialog);


        }

        static string File()
        {
            var rand = new Random();
            var files = Directory.GetFiles("C:/Users/Administrator/Desktop/top/", "*.jpg");
            return files[rand.Next(files.Length)];
        }
        private async Task ResumeAfterOptionDialog(IDialogContext context, IAwaitable<object> result)
        {

        }


    }

}