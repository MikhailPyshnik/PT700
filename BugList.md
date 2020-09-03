#  Task 1: Unit Testing Results

---
# Время, затраченное на выполнение задания - 12 ч. Время сверх нормы - чтение документации Nunit.
--

# Список найденных дефектов:
1) Класс File. Нет доступа к полям content и extension. Нет возможноcти к ним обратиться.

2) В конструкторе класса File возможно задать пустое имя (пустая строка), что недопустимо. В конструкторе нужно добавить проверку - if (string.IsNullOrWhiteSpace(fileName)).

3) Добавить в конструктор для класса FileNameAlreadyExistsException описание ошибки. Чтобы текст ошибки был более читаемым и понятным.
Cейчас: {"Exception of type 'FileSystem.exception.FileNameAlreadyExistsException' was thrown."}.

4) В контсрукторе класса FileStorage нет проверки для параметра Size. Возможно задать отрицательное значение.

