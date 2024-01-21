import pol_regression
from bottle import get,post, route,request, response, run
from json import dumps


model = pol_regression.create_model()


@route('/predict')
def predict():
    # pulls = request.query.pulls
    # rv = {f"{rollnum}": model.predict_chance(rollnum)[0] for rollnum in range(0,91)}
    rv = model.legendary_chances_by_rollnum
    # chance = model.predict_chance(pulls)[0]
    # rv = {"chance":chance}
    # print(rv)
    response.content_type = 'application/json'
    return dumps(rv)
    # return model.predict_chance(pulls)[0]
@route('/data')
def data():
    rv = model.all_pulls
    response.content_type = 'application/json'
    return dumps(rv)

run(host='localhost', port=8080)