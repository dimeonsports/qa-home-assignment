Feature: Rejected cards

  Scenario Outline: Card is rejected due to invalid data
    Given card payload is:
      | Owner      | Number   | Date   | Cvv |
      | Jim Carrey | <number> | <date> | <cvv> |
    When card validation request is sent
    Then status code is 400
    And response includes "<reason>"

    Examples:
      | number             | date  | cvv    | reason |
      | 411111111111111X   | 03/27 | 321    | number |
      | 4111111111111111   | 02/22 | 321    | date   |
      | 4111111111111111   | 03/27 | 12     | cvv    |
      | 4111111111111111   | 03/27 | 12345  | cvv    |