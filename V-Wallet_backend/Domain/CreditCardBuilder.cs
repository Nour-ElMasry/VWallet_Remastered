namespace Domain;
public static class CreditCardBuilder
{
    private static readonly Random random = new Random();
    public static String GenerateIban()
    {
        string countryCode = "RO";
        string checksum = "00";
        string bankAccountNumber = "";

        int bankAccountNumberLength = 16;
        for (int i = 0; i < bankAccountNumberLength; i++)
        {
            bankAccountNumber += random.Next(0, 10).ToString();
        }

        string moduloInput = checksum + bankAccountNumber + "25" + countryCode;
        int moduloResult = 0;
        for (int i = 0; i < moduloInput.Length; i++)
        {
            if (Char.IsDigit(moduloInput[i]))
            {
                int digit = int.Parse(moduloInput[i].ToString());
                moduloResult = (moduloResult * 10 + digit) % 97;
            }
        }
        int checksumValue = 98 - moduloResult;

        checksum = checksumValue.ToString().PadLeft(2, '0');
        return countryCode + checksum + bankAccountNumber;
    }

    public static DateTime GenerateExpirationDate()
    {
        DateTime currentDate = DateTime.Now.Date;

        DateTime minimumDate = currentDate.AddYears(3);
        minimumDate = new DateTime(minimumDate.Year, minimumDate.Month, 1).AddMonths(1).AddDays(-1);
        DateTime maximumDate = currentDate.AddYears(6);
        maximumDate = new DateTime(maximumDate.Year, maximumDate.Month, 1).AddMonths(1).AddDays(-1);

        TimeSpan timeSpan = maximumDate - minimumDate;
        int months = timeSpan.Days / 30;

        int randomMonths = random.Next(0, months);

        DateTime expDate = minimumDate.AddMonths(randomMonths);

        return expDate;
    }

    public static long GenerateCVV()
    {
        return random.Next(100, 1000);
    }

    public static long GenerateDeposit()
    {
        return random.Next(500, 10000);
    }
}
