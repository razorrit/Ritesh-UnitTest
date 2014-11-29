SimpleFixture
=============

SimpleFixture is a portable class library that helps provide test data for unit tests and integration tests. The fixture satisfies these three basic use cases.

* Locate instance - Creates a new instance of the requested type. It does not populate public properies. 
```C#
var fixture = new Fixture();

var instance = fixture.Locate<SomeClass>();
```
* Populate instance - Populate all public properties with randomly created data for a given instance
```C#
var fixture = new Fixture();

fixture.Populate<SomeClass>(instance);
```
* Generate instance - Create a new instance of a given type and populate all public properties.
```C#
var fixture = new Fixture();

var instance = fixture.Generate<SomeClass>();
```

###Return

It's useful sometimes to control what values are returned from the fixture. The Return method offers a way to specify what to return for a give type. Below are some examples.

```C#
var fixture = new Fixture();

// return the value 1 when ever an int it needed
fixture.Return(1);

// return the sequence 1, 2, 3, 1, 2, 3 when satisfying and the request for an int 
fixture.Return(1, 2, 3);

// return incrementing sequence when an int is requested.
int i = 1;
fixture.Return(() => i++);
```

Return also offers the ability to be more granular when returning as seen in the examples below. Note you can chain return filters together to be even more granular
```C#
var fixture = new Fixture();

// return the value 1 when being used to construct SomeClass
fixture.Return(1).For<SomeClass>();

// return incrementing value for int fields ending in Id
int i = 1;
fixture.Return(() => i++).WhenNamed(n => n.EndsWith("Id"));

// return SomeString when matchingMethod returns true
fixture.Return("SomeString").WhenMatching(matchingMethod);
```

###Constraints

Sometimes it's useful to have more control over how an object is created. The generate method takes a constraints object allowing to specify min,max, and other options.
```C#
// Generate an int between 10 and 100
var intValue = fixture.Generate<int>(constraints: new { min = 10, max = 100 });

// Generate constrained date
var dateValue = fixture.Generate<DateTime>(constraints: new { min = dateMin, max = dateMax });

// Set property SomeProperty to value 123 for any int property named SomeProperty
var instance = fixture.Generate<SomeClass>(constraints: new { SomeProperty = 123 });
```

###Customization

Customization offers you the ability to control how an object gets constructed and populated when Return and Constraints aren't enough. 

```C#
var fixture = new Fixture();

// customize how a new instance is created
fixture.Customize<SomeClass>().New(r => new SomeClass());

// customize creation for a new instance with a random int and string
fixture.Customize<SomeOtherClass>().NewFactory<int,string>((i,s) = new SomeOtherClass(i,s,"HardCoded"));

// call the Initialize method everytime a new instance is created
fixture.Customize<SomeClass>().Apply(x => x.Initialize());
```

###Requested Name

To help provide more context on how a type should be create you can provide a request name. Below are some example of currently supported request names for strings

```C#
var fixture = new Fixture();

// generate a first name
var firstName = fixture.Generate<string>("FirstName");

// gernate a last name
var lastName = fixture.Generate<string>("LastName");

// generate a random password with 1 Upper, 1 Lower, 1 Special character and a minimium of 8 characters
var password = fixture.Generate<string>("Password");

// generate a random email for the specified domain
var email = fixture.Generate<string>("EmailAddress", constraints: new { domain = "gmail.com" });

// generate an address line 1
var addressLine1 = fixture.Generate<string>("AddressLine1");
```

###Mocking

Currently Moq and NSubstitute are supported allowing you to automatically mock any missing interfaces
```C#
// MoqFixture is in SimpleFixture.Moq
var fixture = new MoqFixture();

// SubFixture is in SimpleFixture.NSubstitute
var fixture = new SubFixture();
```
