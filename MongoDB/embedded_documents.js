//Query documents using nested /embedded documents
//Complete nested document is required 

db.products.find({
    category:{
        "cid":"C2",
        "cname" : "Vegetables",
        "description" : "Vegetable items"
    }
});

//another method, dot notation, only one field is enough
db.products.find({
    "category.cid":"C2"
});

db.products.find({
    "category.cid":{$eq:"C2"}
})

