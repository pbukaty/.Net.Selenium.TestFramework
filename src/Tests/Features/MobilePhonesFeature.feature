Feature: MobilePhonesFeature
	This test feature demonstrates how to use this framework

Scenario: All step types example
#======== Actions ==============
	Given I navigate to page 'Url'
	When I click on 'WebElement' element
	When I move cursor to 'WebElement' element
	When I set 'WebElement' element selected state to 'true OR false'
	When I type 'text' into element 'WebElement'
	When I select 'text' item from 'DropDown' dropdown by 'text' selection type
	When I select 'value' item from 'DropDown' dropdown by 'value' selection type
	When I select 'index' item from 'DropDown' dropdown by 'index' selection type
#======== Verifications ========
	Then I verify that 'PageName' page is loaded
	Then I verify 'WebElement' element is exist
	Then I verify 'WebElement' element is not exist
	Then I verify that 'WebElement' element is displayed
	Then I verify that elements are displayed:
		| element1    | element2    |
		| WebElement1 | WebElement2 |
	Then I verify that 'WebElement' element is not displayed
	Then I verify that elements are not displayed:
		| element1    | element2    |
		| WebElement1 | WebElement2 |
	Then I verify that 'WebElement' element is enabled
	Then I verify that elements are enabled:
		| element1    | element2    |
		| WebElement1 | WebElement2 |
	Then I verify that 'WebElement' element is not enabled
	Then I verify that elements are not enabled:
		| element1    | element2    |
		| WebElement1 | WebElement2 |
	Then I verify that 'WebElement' element is selected
	Then I verify that elements are selected:
		| element1    | element2    |
		| WebElement1 | WebElement2 |
	Then I verify that 'WebElement' element is not selected
	Then I verify that elements are not selected:
		| element1    | element2    |
		| WebElement1 | WebElement2 |

#================================================

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


Scenario: Use mobile phones filter
	Given I navigate to page 'https://catalog.onliner.by/mobile'
	Then I verify that 'MobilePhonesPage' page is loaded
	Then I verify 'sonyCheckBox' element is not exist
	When I set 'xiaomiCheckBox' element selected state to 'true'
	Then I verify that 'xiaomiCheckBox' element is selected
	And I verify that 'sonyCheckBox' element is not selected
	When I type '300' into element 'maxAmountTextBox'
	And I select '5.8"' item from 'screenSizeMaxDropDown' dropdown by 'text' selection type
	And I select '2gb' item from 'ramMinDropDown' dropdown by 'value' selection type
	And I select '18' item from 'screenResolutionMinDropDown' dropdown by 'index' selection type