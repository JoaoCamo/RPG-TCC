from flask import Blueprint, request, jsonify

main_story_blueprint = Blueprint('main_story', __name__)

@main_story_blueprint.route('/', methods=['POST'])
def main_story():
    return