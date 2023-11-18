/**
 * Тип числа: арабское или римское.
 */
public enum DigitType {
    Arabic(1), Roman(2);
    int num;
    DigitType(int num){
        this.num = num;
    }
}
