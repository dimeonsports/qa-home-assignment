Feature: Accepted cards

  Scenario Outline: Valid card is accepted
    Given card payload is:
      | Owner       | Number   | Date  | Cvv |
      | Jim Carrey  | <number> | <date>| <cvv> |
    When card validation request is sent
    Then status code is 200
    And response includes "<code>"

    Examples:
      | number            | date  | cvv  | code |
      | 4111111111111111  | 03/27 | 321  | 10   |
      | 5111111111111111  | 11/26 | 456  | 20   |
      | 371111111111111   | 08/29 | 9876 | 30   |