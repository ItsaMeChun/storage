using dotenv.net;
using dotenv.net.Utilities;


namespace hcode.Data
{
    public class TestEnv
    {
        String value = EnvReader.GetStringValue("JWTSecretKey");
    }
}
