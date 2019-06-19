
//create or use use existing db
use hexdb;

//create a collection
db.createCollection('products');

//list all collection names
db.getCollectionNames();

//insert a single document
db.products.insert({
    "pid": 1,
    "pname": "Apple",
    "price": 200,
    "quantity": 10,
    "availability": "Available",
    "category": {
        "cid": "C1",
        "cname": "Fruits",
        "description": "Fruits items"
    }
});

//insert multiple documents using insert() method
db.products.insert([{
    "pid": 2,
    "pname": "Orange",
    "price": 100,
    "quantity": 15,
    "availability": "Available",
    "category": {
        "cid": "C1",
        "cname": "Fruits",
        "description": "Fruits items"
    }
},

{
    "pid": 3,
    "pname": "Watermelon",
    "price": 30,
    "quantity": 5,
    "availability": "Out of Stock",
    "category": {
        "cid": "C1",
        "cname": "Fruits",
        "description": "Fruits items"
    }
}]);

//display documents
db.getCollection("products").find();
db.products.find();

//insert a single document using insertOne() method, works only in version > 3.0
db.products.insertOne({
    "pid": 4,
    "pname": "Tomato",
    "price": 20,
    "quantity": 100,
    "availability": "Available",
    "category": {
        "cid": "C2",
        "cname": "Vegetables",
        "description": "Vegetable items"
    }
});

//insert a multiple documents using insertMany() method, works only in version > 3.0
db.products.insertMany([{
    "pid": 5,
    "pname": "Brinjal",
    "price": 25,
    "quantity": 10,
    "availability": "Out of Stock",
    "category": {
        "cid": "C2",
        "cname": "Vegetables",
        "description": "Vegetable items"
    }
},

{
    "pid": 6,
    "pname": "Egg",
    "price": 5,
    "quantity": 20,
    "availability": "Available",
    "category": {
        "cid": "C3",
        "cname": "Food",
        "description": "Food items"
    }
}]);

//insert documents using save method
db.products.save({
    "pid": 7,
    "pname": "Wheat",
    "price": 45,
    "quantity": 25,
    "availability": "Out of Stock",
    "category": {
        "cid": "C3",
        "cname": "Food",
        "description": "Food items"
    }
});

//insert multiple records using save method
db.products.save([{
    "pid": 8,
    "pname": "HP Envy",
    "price": 75000,
    "quantity": 5,
    "availability": "Out of Stock",
    "category": {
        "cid": "C4",
        "cname": "Gadgets",
        "description": "Laptops and mobiles"
    }
},

{
    "pid": 9,
    "pname": "Honor 9 Lite",
    "price": 19999,
    "quantity": 5,
    "availability": "Available",
    "category": {
        "cid": "C4",
        "cname": "Gadgets",
        "description": "Laptops and mobiles"
    }
},

{
    "pid": 10,
    "pname": "Iphone X",
    "price": 86000,
    "quantity": 2,
    "availability": "Out of Stock",
    "category": {
        "cid": "C4",
        "cname": "Gadgets",
        "description": "Laptops and mobiles"
    }
}]);

//display all records
db.products.find();

//drop collection
db.products.drop();


//Update documents using update method, 

//db.products.update(query,update, options)

//Replace the existing record with a new one, do not use multi:true in options

db.products.update({ pid: 1 }, {

    "pid": 1,

    "pname": "Brocoli",

    "price": 20,

    "quantity": 13,

    "availability": "Available"

}, {

        upsert: true,

        //multi:true

    });


//Update certain fields only

db.products.update({ pid: 1 }, {

    $set: {

        "category": {

            "cid": "C2",

            "cname": "Vegetables",

            "description": "Vegetable items"

        }

    }

}, {

        upsert: true,

        //multi:true

    });


db.products.update({ pid: 1 }, {

    $set: { "price": 50 },

    $unset: { "quantity": "" }

}, {

        upsert: true,

        //multi:true

    });

