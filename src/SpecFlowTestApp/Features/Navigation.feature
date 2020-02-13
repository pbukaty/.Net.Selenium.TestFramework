Feature: Navigation
	This test feature demonstrates how to use this framework

Scenario: Navigate to Mobile phone page
	Given I navigate to page 'https://catalog.onliner.by/'
	Then I verify that 'CatalogPage' page is loaded
	When I click on 'electronics' element
	Then I verify that elements are displayed:
		| element1                 | element2 | element3       |
		| mobilePhones&accessories | tv&video | tablets&ebooks |
	When I move cursor to 'mobilePhones&accessories' element
	And I click on 'mobilePhones' element
	Then I verify that 'MobilePhonesPage' page is loaded

Scenario: Navigate to Mobile phone FAIL
	Given I navigate to page 'https://catalog.onliner.by/'
	Then I verify that 'CatalogPage' page is loaded
	When I click on 'electronics' element
	Then I verify that elements are displayed:
		| element1                         | element2 | element3       |
		| mobilePhones&accessories_INVALID | tv&video | tablets&ebooks |
	When I move cursor to 'mobilePhones&accessories_INVALID' element
	And I click on 'mobilePhones' element
	Then I verify that 'MobilePhonesPage' page is loaded