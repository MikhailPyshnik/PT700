#  Task 1: Unit Testing Results

---
# Время, затраченное на выполнение задания - 12 ч. Время сверх нормы - чтение документации Nunit.
--

# Список найденных дефектов:
1) Класс File. Нет доступа к полям content и extension. Нет возможнотси к ним обратиться.

2) В конструктор класса File возможно задать пустое имя (пустая строка) что недопустимо. В конструкторе нужно доабвить проверку - if (string.IsNullOrWhiteSpace(fileName)).

3) Добавить в конструктор для класса ошибки  FileNameAlreadyExistsException описание ошибки. Чтобы текст ошибки был более удобо читаемый и понятным.
Cейчас: {"Exception of type 'FileSystem.exception.FileNameAlreadyExistsException' was thrown."}.

4) В контсруктор FileStorage нет проверки для параметра Size. Возможно задать отрицательное значение.

