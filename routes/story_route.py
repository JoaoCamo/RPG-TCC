from flask import Blueprint, request, jsonify

story_blueprint = Blueprint('story', __name__)

@story_blueprint.route('/', methods=['POST'])
def story():
    return