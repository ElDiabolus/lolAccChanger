using RiotNet;
using RiotNet.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Lol_Account_Changer
{
    class Data
    {
        private List<User> liUser;
        private RiotClient objClient;

        public Data()
        {
            objClient = new RiotClient(Region.EUW, new RiotClientSettings
            {
                ApiKey = "RGAPI-D81F59A3-F444-4353-B5A7-86BCC1985D49"
            });

            liUser = new List<User>();
        }

        public List<User> readFile()
        {
            try
            {
                // Open the text file using a stream reader.
                using (StreamReader sr = new StreamReader("user.ini"))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        loadUser(line);
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Die Datei wurde nicht gefunden oder es ist ein Fehler aufgetreten.");
                MessageBox.Show(e.ToString());
            }

            return liUser;
        }

        public string getLolPath()
        {
            string strPfad = "";
            try
            {
                // Open the text file using a stream reader.
                using (StreamReader sr = new StreamReader("lolpath.ini"))
                {
                    strPfad = sr.ReadLine();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Die Datei wurde nicht gefunden oder es ist ein Fehler aufgetreten.");
                MessageBox.Show(e.ToString());
                Application.Current.Shutdown();
            }

            return strPfad;
        }

        public void loadUser(string strUserstring)
        {
            string[] arrDaten;
            string[] stringSeparators = new string[] { ":" };
            arrDaten = strUserstring.Split(stringSeparators, StringSplitOptions.None);

            User objUser = new User();
            objUser.setUserID(liUser.Count);
            objUser.setUsername(arrDaten[0]);
            objUser.setPasswort(arrDaten[1]);
            objUser.setSummonerName(arrDaten[2]);

            //Spielerdaten holen
            try
            {
                var objSummoner = objClient.GetSummonerBySummonerName(objUser.getSummonerName());
                Dictionary<string, List<League>> listLeague = objClient.GetLeaguesBySummonerIds(objSummoner.Id);
                objUser.setLevel(objSummoner.SummonerLevel);
                try
                {
                    objUser.setLiga(listLeague.First().Value[0].Tier.ToString());
                }
                catch(Exception)
                {
                    objUser.setLiga("UNRANKED");
                }
                Dictionary<string, List<League>> dicLeague = objClient.GetLeagueEntriesBySummonerIds(objSummoner.Id);

                try
                {
                    if (objUser.getLiga() == "CHALLENGER" || objUser.getLiga() == "MASTER")
                    {
                        objUser.setDivision("");
                    }
                    else
                    {
                        objUser.setDivision(dicLeague.First().Value[0].Entries[0].Division);
                    }
                }
                catch (Exception)
                {
                    objUser.setDivision("");
                }

                liUser.Add(objUser);
            }
            catch(Exception)
            {
                MessageBox.Show("Der Summoner "+ objUser.getSummonerName() + " konnte nicht gefunden werden.");
                Application.Current.Shutdown();
            }
        }
    }
}
