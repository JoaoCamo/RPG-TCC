from flask import Blueprint, request, jsonify

dungeon_blueprint = Blueprint('dungeon', __name__)

@dungeon_blueprint.route('/', methods=['POST'])
def dungeon():
    return