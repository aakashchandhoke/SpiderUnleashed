using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading.Tasks;
using System.IO;

namespace SpiderUnleashed {
    public static class HelperFunctions {
        public static WebProxy CreateWebProxy(string server, int port, string username, string password) {

            WebProxy webProxy = null;
            if (server != null) {

                string temp = server + ":" + port;
                webProxy = new WebProxy(temp, true);

                if (username != null && password != null) {
                    webProxy.Credentials = new NetworkCredential(username, password);
                }

            }
            return webProxy;

        }

        public static List<string> GetAllFilesFromFolder(string root, bool searchSubfolders) {
            Queue<string> folders = new Queue<string>();
            List<string> files = new List<string>();
            folders.Enqueue(root);
            while (folders.Count != 0) {
                string currentFolder = folders.Dequeue();
                try {
                    string[] filesInCurrent = System.IO.Directory.GetFiles(currentFolder, "*.*", System.IO.SearchOption.TopDirectoryOnly);
                    files.AddRange(filesInCurrent);
                }
                catch {
                    // Do Nothing
                }
                try {
                    if (searchSubfolders) {
                        string[] foldersInCurrent = System.IO.Directory.GetDirectories(currentFolder, "*.*", System.IO.SearchOption.TopDirectoryOnly);
                        foreach (string _current in foldersInCurrent) {
                            folders.Enqueue(_current);
                        }
                    }
                }
                catch {
                    // Do Nothing
                }
            }
            return files;
        }

        public static string GetDesktopPath() {
            return (Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory));
        }

        public static WebProxy GetProxyFromFile(string FilePath) {

            WebProxy webProxy = null;

            if (File.Exists(FilePath)) {

                string[] contents = File.ReadAllLines(FilePath);

                string pAddress = contents[0];
                int pPort = int.Parse(contents[1]);
                string pUser = contents[2];
                string pPass = contents[3];

                webProxy = CreateWebProxy(pAddress, pPort, pUser, pPass);
            }

            return webProxy;

        }
    }
}
