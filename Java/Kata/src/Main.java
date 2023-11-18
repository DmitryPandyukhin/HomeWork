import java.util.regex.Matcher;
import java.util.regex.Pattern;

public class Main {
    public static void main(String[] args) {
        TestCalc();
    }

    /**
     * Метод выполняет одну из указанных арифметических операций: '+', '-', '*', '/'.
     * @param input Строка с арифметическим выражением между двумя числами.
     *              Числа могут быть только римскими или только арабскими.
     * @return Результат арифметический операции.
     */
    public static String calc(String input) {
        try {
            Pattern pat1 = Pattern.compile(" *[0-9]+ *[+\\-*/] *[0-9]+ *");
            Matcher mat1 = pat1.matcher(input);
            Pattern pat2 = Pattern.compile(" *[ivxlcdmIVXLCDM]+ *[+\\-*/] *[ivxlcdmIVXLCDM]+ *");
            Matcher mat2 = pat2.matcher(input);
            // Проверка формата операции.
            if(!mat1.matches()){
              if(!mat2.matches()){
                  throw new Exception("Формат математической операции не удовлетворяет заданию - два операнда и один оператор (+, -, /, *);");
              }
              // Проверка корректности римскоих чисел.
              else{
                  int maxLen = 0;

                  Matcher mat3 = Pattern.compile("([ivxlcdmIVXLCDM])\\1+").matcher(input);
                  while (mat3.find()) {
                      String sub = mat3.group();

                      if (sub.length() > maxLen) {
                          maxLen = sub.length();
                      }
                  }

                  if(maxLen > 3)
                      throw new Exception("Некорректно задано римское число, " +
                              "т.к. количество последовательно повторяющихся цифр должно быть не больше 3.");
              }
            }

            StringBuilder num1 = new StringBuilder();
            StringBuilder num2 = new StringBuilder();
            char ch;
            DigitType digitType = null;
            OperationType operationType = null;
            int result = 0;

            // Форматируем данные.
            input = input.trim().toUpperCase();

            for (int i = 0; i < input.length(); i++) {
                ch = input.charAt(i);

                // Если символ является арабской цифрой.
                if (Character.isDigit(ch)) {
                    if (digitType == null) digitType = DigitType.Arabic;

                    // Определяем и заполняем римские цифры.
                    if (operationType == null) num1.append(ch);
                    else num2.append(ch);
                }

                // Если символ является римской цифрой.
                if (RomanDigit.getRomanDigit(ch) != null) {
                    if (digitType == null)
                        digitType = DigitType.Roman;

                    if (operationType == null) num1.append(ch);
                    else num2.append(ch);
                }

                // Если символ является арифметической операцией.
                if (OperationType.isOperation(ch)) {
                    operationType = OperationType.getOperatopnType(ch);
                }
            }

            if (digitType == DigitType.Arabic) {
                int num1Int = Integer.parseInt(num1.toString());
                int num2Int = Integer.parseInt(num2.toString());

                // Проверяем диапазон введенных чисел/
                if ((num1Int > 10) || (num1Int < 1) || (num2Int > 10) || (num2Int < 1))
                    throw new Exception("Заданные числа выходят за пределы диапазона [1, 10].");

                // Вычисляем результат.
                switch (operationType) {
                    case add -> result = num1Int + num2Int;
                    case sub -> result = num1Int - num2Int;
                    case mul -> result = num1Int * num2Int;
                    case div -> result = num1Int / num2Int;
                }
                return String.valueOf(result);
            }

            if (digitType == DigitType.Roman) {
                var rn1 = new RomanNumber(num1.toString());
                var rn2 = new RomanNumber(num2.toString());
                int num1Int = ArabicNumber.toArabicNumber(rn1);
                int num2Int = ArabicNumber.toArabicNumber(rn2);

                if((operationType == OperationType.mul) && (num2Int > num1Int))
                    throw new Exception("Результат операции должен быть положительным.");

                // Вычисляем результат.
                switch (operationType) {
                    case add -> result = num1Int + num2Int;
                    case sub -> result = num1Int - num2Int;
                    case mul -> result = num1Int * num2Int;
                    case div -> result = num1Int / num2Int;
                }

                return RomanNumber.toRomanNumber(result);
            }

        } catch (Exception e) {
            System.out.println(e.getMessage());
        }
        return "";
    }

    public static void TestCalc(){
        // Строка для поиска сочетаний повторяющихся символов.
        //String str = " ivxlcdmIVXLCDM1234567890+-*/";
        String str = " /iX";
        // Максимальная длина получаемой строки
        int max = 6;

        char[] arr = str.toCharArray();
        for(int i = 0; i<=max; i++)
            permutationNOfK(i, arr);
    }

    /**
     * Метод печати всех возможных сочетаний символов с посторениями.
     * @param k Длина печатаемой строки.
     * @param arr Массив символов.
     */
    public static void permutationNOfK(int k, char[] arr) {
        int n = arr.length;
        var inputStr = new StringBuilder();
        var outputStr = new StringBuilder();

        int[] perm = new int[k];
        for (;;) {
            for(int i=0; i < perm.length; i++)
                inputStr.append(arr[perm[i]]);
                //System.out.print(arr[perm[i]]);


            System.out.println("Input:");
            System.out.println(inputStr);
            System.out.println("Output:");
            // Вычисляем и печатаем.
            outputStr.append(calc(inputStr.toString()));
            if(!outputStr.isEmpty())
                System.out.println(outputStr);
            System.out.println();

            // Обнуляем
            inputStr.setLength(0);
            outputStr.setLength(0);

            //System.out.println();
            //System.out.println(Arrays.toString(perm));
            int i = k - 1;
            for (; i >= 0; i--) {
                if (perm[i] < n - 1) {
                    break;
                }
            }
            if (i < 0) {
                break;
            }
            perm[i] += 1;
            i = i + 1;
            for (; i < k;) {
                perm[i] = 0;
                i += 1;
            }
        }
    }
}
