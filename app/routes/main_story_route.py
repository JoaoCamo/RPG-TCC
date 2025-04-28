from flask import Blueprint, request, jsonify
from app.services.main_story_service import generate_main_story

main_story_blueprint = Blueprint("story", __name__)


@main_story_blueprint.route("/", methods=["POST"])
def main_story():
    data = request.get_json()
    type = data.get("type")
    name = data.get("name")
    main_story = generate_main_story(type, name)
    return jsonify({"main_story": main_story})
