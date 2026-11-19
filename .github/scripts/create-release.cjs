module.exports = async function createRelease({ github, context, core }) {
    const repository = context.repo;
    const commit = context.payload.pull_request.merge_commit_sha;
    const releases = await github.paginate(github.rest.repos.listReleases, repository);

    if (releases.some(release => !release.draft && release.target_commitish === commit)) {
        core.info(`A release already exists for ${commit}.`);
        return;
    }

    const tags = await github.paginate(github.rest.repos.listTags, repository);
    const versions = tags
        .map(tag => ({ name: tag.name, match: /^v(\d+)\.(\d+)\.(\d+)$/.exec(tag.name) }))
        .filter(tag => tag.match)
        .map(tag => ({ name: tag.name, parts: tag.match.slice(1).map(Number) }))
        .sort((left, right) =>
            right.parts[0] - left.parts[0] ||
            right.parts[1] - left.parts[1] ||
            right.parts[2] - left.parts[2]);

    if (versions.length === 0) {
        throw new Error("Create and push an initial vMAJOR.MINOR.PATCH tag before releasing.");
    }

    const previous = versions[0];
    const [major, minor, patch] = previous.parts;
    const next = `v${major}.${minor}.${patch + 1}`;
    const notes = await github.rest.repos.generateReleaseNotes({
        ...repository,
        tag_name: next,
        target_commitish: commit,
        previous_tag_name: previous.name,
    });

    const release = await github.rest.repos.createRelease({
        ...repository,
        tag_name: next,
        target_commitish: commit,
        name: next,
        body: notes.data.body,
        draft: false,
        prerelease: false,
    });

    core.info(`Created ${release.data.html_url}`);
};