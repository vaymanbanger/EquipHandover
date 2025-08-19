# Автоматизация акта о приеме-передачи оборудования
![Изображение](https://guravskoe.ru/wp-content/images/akt-priema-peredachi-oborudovaniya-mezhdu-fizicheskimi-litsami-prostoj-obra.jpg)
## Схема базы данных
```mermaid
erDiagram
    DOCUMENT {
        uuid Id
        dateonly RentalDate
        string SignatureNumber
        string City
        uuid SenderId
        uuid ReceiverId
    }
    
    SENDER {
        uuid Id
        string FullName
        string Enterprise
        string Inn
    }
    
    RECEIVER {
        uuid Id
        string FullName
        string Enterprise
        string Ogrn
    }
    
    EQUIPMENT {
        uuid Id
        string Name
        int ManufactureDate
        string SerialNumber
        string EquipmentNumber
    }
    
    DOCUMENTEQUIPMENT {
        uuid DocumentId
        uuid EquipmentId
        int Quantity
    }
    
    DOCUMENT ||--|| SENDER : "имеет"
    DOCUMENT ||--|| RECEIVER : "имеет"
    DOCUMENT ||--|{ DOCUMENTEQUIPMENT : "содержит"
    EQUIPMENT ||--o{ DOCUMENTEQUIPMENT : "входит в"
```


## Реализация API
### CRUD документов
|verb|url|description|request|response|codes|
|-|-|-|-|-|-|
|GET|api/documents/|Получает список всех документов| |`[docApiModel]`| 200 OK |
|GET|api/documents/{id}|Получает документ с идентификатором id| fromRoute: id |`docApiModel`| 200 OK<br/>404 Not Found |
|GET|api/documents/{id}/export|Экспортирует документ в Excel| fromRoute: id |Файл Excel| 200 OK<br/>404 Not Found |
|POST|api/documents/|Добавляет новый документ| fromBody: `docRequestApiModel`|`docApiModel`| 200 OK |
|PUT|api/documents/{id}|Редактирует документ с идентификатором id| fromRoute: id <br/>fromBody: `docRequestApiModel`|`docApiModel`| 200 OK<br/>404 Not Found |
|DELETE|api/documents/{id}|Удаляет документ с идентификатором id| fromRoute: id | | 200 OK<br/>404 Not Found |
```javascript
// docApiModel
{
    Id: 1fba52c2-17c5-4731-aca0-e52247f2629,
    RentalDate: "16.08.2025",
    SignatureNumber: "АКТ-2025-567",
    City: "Санкт-Петербург",
    SenderId: 57ebb48e-3093-4ac3-96ef-43d6dc36c744,
    SenderFullName: "Иванов Иван Иванович",
    SenderEnterprise: "ООО ПЕТРОВИЧ",
    SenderInn: 520205004556,
    ReceiverId: 79c2b608-3455-4e87-be7d-18807a930505,
    ReceiverFullName: "Иванов Иннокентий Иванович",
    ReceiverEnterprise: "ООО Михалыч",
    ReceiverOgrn: 1147847423899,
    Equipment: [
        {
            EquipmentId: a624c88b-178c-4b9f-a67a-4541d5797f15,
            Name: "Ноутбук",
            ManufactureDate: 2025,
            SerialNumber: "HS235AAA2",
            EquipmentNumber: "PROD-2025"
        }
    ]
}
```
```javascript
// docRequestApiModel
{
    RentalDate: "16.08.2025",
    SignatureNumber: "АКТ-2025-567",
    City: "Санкт-Петербург",
    SenderId: 57ebb48e-3093-4ac3-96ef-43d6dc36c744,
    ReceiverId: 79c2b608-3455-4e87-be7d-18807a930505
    Equipment: [
        {
            EquipmentId: a624c88b-178c-4b9f-a67a-4541d5797f15,
            Name: "Ноутбук",
            ManufactureDate: 2025,
            SerialNumber: "HS235AAA2",
            EquipmentNumber: "PROD-2025"
        }
    ]
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
    Id: 1fba52c2-17c5-4731-aca0-e52247f2629,
    Name: "Ноутбук",
    ManufactureDate: 2025,
    SerialNumber: "HS235AAA2",
    EquipmentNumber: "PROD-2025"
}
```
```javascript
// equipRequestApiModel
{
    Name: "Ноутбук",
    ManufactureDate: 2025,
    SerialNumber: "HS235AAA2",
    EquipmentNumber: "PROD-2025"
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
    Id: 1fba52c2-17c5-4731-aca0-e52247f2629,
    FullName: "Иванов Иван Иванович",
    Enterprise: "ООО ПЕТРОВИЧ"
    Inn: 520205004556
}
```
```javascript
// senderRequestApiModel
{
    FullName: "Иванов Иван Иванович",
    Enterprise: "ООО ПЕТРОВИЧ"
    Inn: 520205004556
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
    Id: 1fba52c2-17c5-4731-aca0-e52247f2629,
    FullName: "Иванов Иван Иванович",
    Enterprise: "ООО ПЕТРОВИЧ"
    Ogrn: 1147847423899
}
```
```javascript
// receiverRequestApiModel
{
    FullName: "Иванов Иван Иванович",
    Enterprise: "ООО ПЕТРОВИЧ"
    Ogrn: 1147847423899
}
```
