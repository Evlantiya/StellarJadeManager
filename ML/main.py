import pol_regression
from bottle import get,post, route,request, response, run
from json import dumps


model = pol_regression.create_model()
print(model.predict_chance(85)[0])
print('av')
@route('/predict')
def predict():
    pulls = request.query.pullsvvv
    chance = model.predict_chance(pulls)[0]
    rv = {"chance":chance}
    response.content_type = 'application/json'
    return dumps(rv)
    # return model.predict_chance(pulls)[0]

run(host='localhost', port=8080)