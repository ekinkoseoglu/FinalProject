using Core.Utilities.Results;

namespace Core.Utilities.Business
{
    public class BusinessRules
    {
        public static IResult Run(params IResult[] logics)
        {
            foreach (var logic in logics) // Methodları döndürsün
            {
                if (!logic.Success) // Eğer dönen method başarısız ise
                {
                    return logic; // Business kuralına döndürsün çünkü o kural methodunun içinde zaten ".Success" false olduğu için kuralın içindeki "ErrorResult" döndürülecek.
                }
            }

            return null;
        }
    }
}
