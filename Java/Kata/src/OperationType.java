/**
 * Перечисление арифметических операций.
 */
public enum OperationType {
    add('+'), sub('-'), mul('*'), div('/');
    final char operation;
    OperationType(char operation){
        this.operation = operation;
    }

    /**
     * Метод проверяет, является ли переданный симво математической операцией.
     * @param operation Проверяемый символ.
     * @return Результат проверки.
     */
    public static boolean isOperation(char operation){
        for(OperationType ot : OperationType.values()){
            if(ot.operation == operation)
                return true;
        }
        return false;
    }

    /**
     * Метод ищем заданную метематическую операцию в перечислении.
     * @param operation Символ математической операции
     * @return Математическая операция.
     */
    public static OperationType getOperatopnType(char operation){
        for(OperationType ot : OperationType.values()){
            if(ot.operation == operation)
                return ot;
        }
        return null;
    }
}
