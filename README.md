PicPay Challenge API

Esta Web API foi desenvolvida para resolver o desafio técnico do PicPay. A API gira em torno de duas entidades principais: Wallet e Transfer. Utilizando conceitos como CQRS, ResultPattern e Notification.

Funcionalidades Principais
Wallet: Representa uma carteira digital de usuário. Existem dois tipos de carteiras:
Common: Pode enviar e receber dinheiro.
Merchant: Pode apenas receber dinheiro.
Transfer: Representa uma transferência de dinheiro entre carteiras.
Arquitetura do Projeto
Neste projeto, utilizamos algumas práticas e padrões de design, incluindo:

ResultPattern: Um padrão para padronizar o retorno das operações, encapsulando o status code, mensagens de erro ou sucesso e dados resultantes.
Notification Pattern: Utilizado para acumular e reportar erros de validação de maneira eficaz.

Validação e Notificações
Os Commands utilizam o padrão Notification para realizar validações antes de serem processados pelos Handlers. No caso de erros de validação, os handlers também acumulam esses erros e os retornam utilizando o padrão BaseResult, que encapsula o status code, mensagem e os dados (que podem ser um objeto de sucesso ou mensagens de erro).

Estrutura de Resposta da API
A estrutura de resposta segue o padrão BaseResult, que contém os seguintes campos:

StatusCode: Código de status HTTP (ex: 200, 400, 404).
Message: Mensagem de sucesso ou erro.
Data (T?): Dados retornados pela operação (por exemplo, uma Wallet criada com sucesso ou uma lista de mensagens de erro).
Exemplo de resposta de sucesso:

json
{
  "statusCode": 201,
  "message": "Wallet created",
  "data": {
    "id": "xxxx-xxxx-xxxx-xxxx",
    "fullName": "Gabriel Wisenhutter",
    ...
  }
}

Exemplo de resposta de erro:
json
{
  "statusCode": 400,
  "message": "One or more errors",
  "data": [
    "An account with this document number already exists",
    "An account with that Email already exists"
  ]
}
