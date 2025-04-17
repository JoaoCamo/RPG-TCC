from flask import Blueprint, request, jsonify

character_blueprint = Blueprint('character_story', __name__)

@character_blueprint.route('/', methods=['POST'])
def character():
    return