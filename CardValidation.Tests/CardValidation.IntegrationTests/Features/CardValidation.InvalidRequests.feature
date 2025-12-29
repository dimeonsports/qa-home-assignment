Feature: Invalid requests

  Scenario Outline: Missing required field returns error
    Given card payload is:
      | Owner       | Number  | Date  | Cvv |
      | <own>       | <num>   | <dat> | <cvv> |
    When card validation request is sent
    Then status code is 400
    And response includes "required"

    Examples:
      | own        | num              | dat   | cvv  |
      |            | 4111111111111111 | 03/29 | 321  |
      | Jim Carrey |                  | 03/29 | 321  |
      | Jim Carrey | 4111111111111111 |       | 321  |
      | Jim Carrey | 4111111111111111 | 03/29 |      |