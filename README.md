Чтобы получить доступ к системе, необходимо пройти аутентификацию и авторизацию вводом логина и пароля (admin/admin user/user).
В приложении есть две роли: управляющий и обычный сотрудник:
•	Управляющий может создавать задачи и назначать их на обычных сотрудников, регистрировать новых сотрудников.
•	Обычный сотрудник может просматривать список назначенных на него задач и менять статус каждой из них. («Предложено», «В работе», «Выполнено»).
У каждой задачи есть идентификационный номер проекта, название и описание. 
данные хранятся в ManagementDB.db  -база на SQLite 
пароли в базе захэшированы 

реализовано сохранение действий пользователя в файл  steps.log и сохранение критических ошибок в errors.log 

man - список всех возможных команд
введите --help для вывода справки по конкретной команде

lu (list users) - список всех пользоваетелей

nu (new user) - создание пользователя

chu (change user) - изменение пользователя

delu (delete user) - удаление пользователя

lt (list tasks) - список всех задач

ltm (list tasks my) - список задач текущего пользователя

nt (new task) - создание задачи, (задача создается в статусе Предложено)

cht (change task) - изменение задачи

ctm (chage tasks my) - изменить статус задачи текущего пользователя

delu (delete task) - удаление задачи

clear - очищение консоли

exit - завершение работы приложения
