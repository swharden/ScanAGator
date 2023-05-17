namespace DendriteTracer.Tests;

internal class SampleData
{
    public static string TSERIES_2CH_PATH = Path.GetFullPath(
        Path.Combine(
            TestContext.CurrentContext.TestDirectory,
            "../../../../../../data/tseries/TSeries-03022023-1227-2098-2ch.tif"));

    [Test]
    public void Test_Paths_Exist()
    {
        if (!File.Exists(TSERIES_2CH_PATH))
            throw new DirectoryNotFoundException(TSERIES_2CH_PATH);
    }
}
