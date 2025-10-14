from flask import Blueprint, request, jsonify
from app.services.main_story_service import generate_main_story

main_story_blueprint = Blueprint("story", __name__)


@main_story_blueprint.route("/", methods=["POST"])
def main_story():
    data = request.get_json()
    type = data.get("type")
    name = data.get("name")
    character_history = data.get("character_history")
    context = data.get("context")
    main_story = generate_main_story(type, name, character_history, context)
    return jsonify(main_story)
