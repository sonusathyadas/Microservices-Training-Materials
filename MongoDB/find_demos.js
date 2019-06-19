//Display all documents
db.products.find();

//Total number of documents
db.products.find().count();

//display a specific number of documents
db.products.find().limit(5);

//display documents after a certain number of documents
db.products.find().skip(5);

//display a set of documents after a number of documents
db.products.find().skip(5).limit(2);
db.products.find().limit(2).skip(5); //same result

//Diplay sorted list of documents
db.products.find().sort({'price':1});
db.products.find().sort({'price':-1});
db.products.find().sort({'price':-1, 'quantity':1});

//Equality conditional operator
db.products.find({'availability':'Available'});
db.products.find({"category.cname":"Fruits"})

//relational operators
db.products.find({
    'availability':{$eq:'Available'}
});

db.products.find({
    'availability':{$ne:'Available'}
});

db.products.find({
    'price':{$gt:20}
    });
db.products.find({
    'price':{$lt:20}
});
db.products.find({
    'price':{$lte:20}
});
db.products.find({
    'price':{$gte:20}
});



//AND and OR and NOT operators
db.products.find({
    $and:[
        {availability:'Available'}, 
        {quantity: {$gte:20}}
    ]
});

db.products.find({
    $or:[
        {"category.cname":"Fruits"},
        {"category.cname":"Vegetables"}
    ]
});

db.products.find({
    'price':{$not:{$gt:25}}
});

//project only certain fields
//include only few fileds
db.products.find({}, {
    "pname":1,
    "price":1,
    "category.cname":1
});
//exclude few fields
db.products.find({},{
    "price":-1,
    "category":-1
})