using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.WindowsAzure.Storage;
using Microsoft.Bot.Builder.Dialogs;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using System.IO;
using Microsoft.Bot.Connector;

namespace TryDresses.Dialogs
{
    [Serializable]
    public class ShowImageDialog : IDialog<object>
    {
        public async Task StartAsync(IDialogContext context)
        {

            try

            {

                CloudStorageAccount account = new CloudStorageAccount
                    (new StorageCredentials(
                        "faceswapaashi",
                        "uj84KLG0nVAkGGJWkh+FUUicLg5lp2ehVd0OFmPpwtEaTdZY+4E3FMVBPG2ijuJdEtl1m/iUNWQojbXa6z0w1w=="),
                        true);
                // We need to access blobs now, so create a CloudBlobClient
                CloudBlobClient blobClient = account.CreateCloudBlobClient();
                CloudBlobContainer container = blobClient.GetContainerReference("output");

                var text = File.ReadAllLines(@"C:\Users\mtcuser\Documents\VITON-master\data\viton_test_pairs.txt");
                string[] t = text[0].Split(' ');
                var photo = t[0];
                var dress = t[1];
                CloudBlockBlob Blob = container.GetBlockBlobReference(t[0] + t[1] + ".jpg");

                // Create or overwrite the "myblob" blob with contents from a local file.
                using (Stream fileStream = System.IO.File.OpenRead(@"C:\Users\mtcuser\Documents\VITON-master\results\stage2\images\" + photo + "_" + dress + "_final.png"))
                {
                    Blob.UploadFromStream(fileStream);
                }
                var replyMessage = context.MakeMessage();
                Attachment attachment = GetInternetAttachment(t[0], t[1]);
                replyMessage.Attachments = new List<Attachment> { attachment };
                await context.PostAsync(replyMessage);
                await context.PostAsync("This is how you look in the dress.");
                context.Call(new RootDialog(), this.ResumeAfterOptionDialog);

            }
            catch (Exception ex)
            {
                await context.PostAsync(ex.Message);

            }



        }
        private static Attachment GetInternetAttachment(string a, string b)
        {
            return new Attachment
            {
                Name = "output.jpg",
                ContentType = "image/jpeg",
                ContentUrl = "https://faceswapaashi.blob.core.windows.net/output/" + a + b + ".jpg"
            };
        }
        private async Task ResumeAfterOptionDialog(IDialogContext context, IAwaitable<object> result)
        {

        }
    }
}
