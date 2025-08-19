# Автоматизация акта о приеме-передачи оборудования
![Изображение](https://guravskoe.ru/wp-content/images/akt-priema-peredachi-oborudovaniya-mezhdu-fizicheskimi-litsami-prostoj-obra.jpg)
## Схема базы данных
```mermaid
erDiagram
DOCUMENT {
    dateonly rentalDate
    string signatureNumber
    string city
}
DOCUMENT ||--o{ SENDER : "имеет"
SENDER {
    string fullName
    string enterprise
    int inn
}
DOCUMENT ||--o{ RECEIVER : "имеет"
RECEIVER {
    string fullName
    string enterprise
    int ogrn
}
DOCUMENT ||--|{ EQUIPMENT : "содержит"
EQUIPMENT {
    string name
    int manufactureDate
    string serialNumber
    string equipmentNumber
}

```


## Реализация API
### CRUD документов
|verb|url|description|request|response|codes|
|-|-|-|-|-|-|
|GET|api/documents/|Получает список всех документов| |`[docApiModel]`| 200 OK |
|GET|api/documents/{id}|Получает документ с идентификатором id| fromRoute: id |`docApiModel`| 200 OK<br/>404 Not Found |
|POST|api/documents/|Добавляет новый документ| fromBody: `docRequestApiModel`|`docApiModel`| 200 OK |
|PUT|api/documents/{id}|Редактирует документ с идентификатором id| fromRoute: id <br/>fromBody: `docRequestApiModel`|`docApiModel`| 200 OK<br/>404 Not Found |
|DELETE|api/documents/{id}|Удаляет документ с идентификатором id| fromRoute: id | | 200 OK<br/>404 Not Found |
```javascript
// docApiModel
{
    id: 1,
    rentalDate: "16.08.2025",
    signatureNumber: "АКТ-2025-567",
    city: "Санкт-Петербург"
}
```
```javascript
// docRequestApiModel
{
    rentalDate: "16.08.2025",
    signatureNumber: "АКТ-2025-567",
    city: "Санкт-Петербург"
}
```


### CRUD оборудования
|verb|url|description|request|response|codes|
|-|-|-|-|-|-|
|GET|api/equipment/|Получает список всего оборудования| |`[equipApiModel]`| 200 OK |
|GET|api/equipment/{id}|Получает оборудование с идентификатором id| fromRoute: id |`equipApiModel`| 200 OK<br/>404 Not Found |
|POST|api/equipment/|Добавляет новое оборудование| fromBody: `equipRequestApiModel`|`equipApiModel`| 200 OK |
|PUT|api/equipment/{id}|Редактирует оборудование с идентификатором id| fromRoute: id <br/>fromBody: `equipRequestApiModel`|`equipApiModel`| 200 OK<br/>404 Not Found |
|DELETE|api/equipment/{id}|Удаляет оборудование с идентификатором id| fromRoute: id | | 200 OK<br/>404 Not Found |
```javascript
// equipApiModel
{
    id: 1,
    name: "Ноутбук",
    manufactureDate: 2025,
    serialNumber: "HS235AAA2",
    equipmentNumber: "PROD-2025"
}
```
```javascript
// equipRequestApiModel
{
    name: "Ноутбук",
    manufactureDate: 2025,
    serialNumber: "HS235AAA2",
    equipmentNumber: "PROD-2025"
}
```
### CRUD отправителя
|verb|url|description|request|response|codes|
|-|-|-|-|-|-|
|GET|api/senders/|Получает список всех отправителей| |`[senderApiModel]`| 200 OK |
|GET|api/senders/{id}|Получает отправителя с идентификатором id| fromRoute: id |`senderApiModel`| 200 OK<br/>404 Not Found |
|POST|api/senders/|Добавляет нового отправителя| fromBody: `senderRequestApiModel`|`senderApiModel`| 200 OK |
|PUT|api/senders/{id}|Редактирует отправителя с идентификатором id| fromRoute: id <br/>fromBody: `senderRequestApiModel`|`senderApiModel`| 200 OK<br/>404 Not Found |
|DELETE|api/senders/{id}|Удаляет отправителя с идентификатором id| fromRoute: id | | 200 OK<br/>404 Not Found |
```javascript
// senderApiModel
{
    id: 1,
    fullName: "Иванов Иван Иванович",
    enterprise: "ООО ПЕТРОВИЧ"
    inn: 520205004556
}
```
```javascript
// senderRequestApiModel
{
    fullName: "Иванов Иван Иванович",
    enterprise: "ООО ПЕТРОВИЧ"
    inn: 520205004556
}
```
### CRUD принимающего
|verb|url|description|request|response|codes|
|-|-|-|-|-|-|
|GET|api/receivers/|Получает список всех принимающих| |`[receiverApiModel]`| 200 OK |
|GET|api/receivers/{id}|Получает принимающего с идентификатором id| fromRoute: id |`receiverApiModel`| 200 OK<br/>404 Not Found |
|POST|api/receivers/|Добавляет нового принимающего| fromBody: `receiverRequestApiModel`|`receiverApiModel`| 200 OK |
|PUT|api/receivers/{id}|Редактирует принимающего с идентификатором id| fromRoute: id <br/>fromBody: `receiverRequestApiModel`|`receiverApiModel`| 200 OK<br/>404 Not Found |
|DELETE|api/receivers/{id}|Удаляет принимающего с идентификатором id| fromRoute: id | | 200 OK<br/>404 Not Found |
```javascript
// receiverApiModel
{
    id: 1,
    fullName: "Иванов Иван Иванович",
    enterprise: "ООО ПЕТРОВИЧ"
    ogrn: 1147847423899
}
```
```javascript
// receiverRequestApiModel
{
    fullName: "Иванов Иван Иванович",
    enterprise: "ООО ПЕТРОВИЧ"
    ogrn: 1147847423899
}
```
### Экспорт документа в Excel
|verb|url|description|request|response|codes|
|-|-|-|-|-|-|
|GET|api/documents/{id}/export|Экспортирует документ в Excel| fromRoute: id |Файл Excel| 200 OK<br/>404 Not Found |
