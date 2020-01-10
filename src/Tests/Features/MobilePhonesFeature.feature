Feature: MobilePhonesFeature
	This test feature demonstrates how to use this framework

@mytag
Scenario: Use mobile phones filter
	Given I navigate to page:
		| url                         | pageName    |
		| https://catalog.onliner.by/ | CatalogPage |
	And I click on 'electronics' element
	Then I verify that elements are displayed:
		| element1                 | element2 | element3       |
		| mobilePhones&accessories | tv&video | tablets&ebooks |
	And I moved cursor to 'mobilePhones&accessories' element
	And I click on 'mobilePhones' element
	Then I verify that 'MobilePhonesPage' page is loaded