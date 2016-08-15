namespace Lol_Account_Changer
{
  public class User
  {
    private string _strUsername;

    private string _strPasswort;

    private long _intLevel;

    private string _strSummonerName;

    private string _strLiga;

    private string _strDivision;

        private int _intUserID;


    public string getUsername()
    {
      return this._strUsername;
    }

    public string getPasswort()
    {
      return this._strPasswort;
    }

    public string getSummonerName()
    {
      return this._strSummonerName;
    }

    public string getLiga()
    {
      return this._strLiga;
    }

    public string getDivision()
    {
      return this._strDivision;
    }

    public long getLevel()
    {
      return this._intLevel;
    }

    public void setUsername(string strUsername)
    {
      this._strUsername = strUsername;
    }

    public void setPasswort(string strPasswort)
    {
      this._strPasswort = strPasswort;
    }

    public void setSummonerName(string strSummonerName)
    {
      this._strSummonerName = strSummonerName;
    }

    public void setLiga(string strLiga)
    {
      this._strLiga = strLiga;
    }

    public void setDivision(string strDivision)
    {
      this._strDivision = strDivision;
    }

    public void setLevel(long intLevel)
    {
      this._intLevel = intLevel;
    }

        public int getUserID()
        {
            return this._intUserID;
        }

        public void setUserID(int intUserID)
        {
            this._intUserID = intUserID;
        }

  }
}