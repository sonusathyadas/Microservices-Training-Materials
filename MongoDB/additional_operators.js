use hexdb

db.createCollection("users");

db.users.insertMany([
    {
        userid:'aditya',
        password:'aditya@123',
        mobile:8943089436,
        hobbies: ['cricket','hockey','football']        
    },
    {
        userid:'james',
        password:'james@123',
        mobile:9748583738,
        hobbies: ['hockey','football','chess']
    },
    {
        userid:'desmond',
        password:'desmond@123',
        mobile:85484848596,
        hobbies: ['cricket','football']
    }
]);

//array operators
//$all operator selects the documents where the value of a field is an array 
//that contains all the specified elements.
    
db.users.find({
    'hobbies':{$all:['hockey','cricket','football']}
});

//$size operator matches any array with the number of elements specified by the argument. 
db.users.find({
    'hobbies':{$size:2}
});

//limit the number of elements while projection using $slice
//$slice accepts arguments in a number of formats, including negative values and arrays.
// $slice selects the first two items in an array in the comments field.
db.users.find({},{hobbies:{$slice:2}})
//Last two items
db.users.find( {}, { hobbies: { $slice: -2 } } )
//Specify the skip and take values as array, pick elemetns from a range
//skip index 0 and start from 1, and pick next 5 items
db.users.find({userid:'aditya'}, {hobbies:{$slice:[1,5]}}); 

