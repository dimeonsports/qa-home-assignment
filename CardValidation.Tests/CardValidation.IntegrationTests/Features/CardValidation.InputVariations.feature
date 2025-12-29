Feature: Input variations

  Scenario Outline: Input variations return expected status
    Given card payload is:
      | Owner       | Number           | Date   | Cvv |
      | Jim Carrey  | 4111111111111111 | <date> | <cvv> |
    When card validation request is sent
    Then status code is <status>

    Examples:
      | date  | cvv  | status |
      | 03/27 | 321  | 200    |
      | 14/27 | 321  | 400    |
      | 00/27 | 321  | 400    |
      | 03/27 | 99   | 400    |